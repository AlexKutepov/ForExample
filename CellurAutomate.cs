
//Смотря чужой код, где не описана суть задачи - иногда тяжело понять,что запрограммировал наш коллега,
//поэтому вводной строкой попытаюсь объяснить суть задачи. 
//Суть задачи в моделирование дорожного движения.
//Зачем тут пресутствует клетка? Она является частью сущности клеточного автомата, который в данном случае помогает в моделирование 
//дорожного движения, из клеток состоит дорога. См. моделирование дорожного движения с использованием клеточного автомата.
//Эту модель делал я,она для однополосного движения. Есть и для многополосного движения и для перекрёстка, она намного сложней...

//ссылка на архитектуру:
//https://sun9-60.userapi.com/impf/reyREqvRK61yFxIHLKwQfyfJLuFCyOJj0wShvw/B6nNE37WcD4.jpg?size=803x504&quality=96&proxy=1&sign=c42d57ea160a62aeb3fc4d2fc1a82f81&type=album

//ссылка на литературу по теме:
//http://bek.sibadi.org/fulltext/epd624.pdf

// p.s. - комметарии в этом коде используются для перевода между языком мат.модели и программистом, т.е. чтобы не запутаться. 
// так как программистом и математиком выступал я - было принято решение не мудрить с названиями переменных.
// Поясню, если использовать понятные людям названия переменных - 
// тогда можно запутаться при программирование самой модели и наоборот. Надеюсь вы меня поняли.
// И да простит меня Роберт Мартин.

public class Cell {

	public Vehicle vehicle_;

	public int MaxRulesSpeed {
		set;
		get;
	}
	public int SpeedInCell {
		set;
		get;
	}
	public float QualityLane {
		set;
		get;
	}

	public int Barrier {
		set;
		get;
	}

	public void Delete() {
		vehicle_ = null;
	}

	public bool IsDelete() {
		return vehicle_ == null;
	}

	public Vehicle GetVehicle() {
		return vehicle_;
	}

	public void SetVehicle(Vehicle vehicle) {
		vehicle_ = vehicle;
	}

}

public class Vehicle {

	private int speed, newspeed;

	private bool stop, newstop_;

	private Cell cell_;

	private int maxSpeed;

	public Vehicle(int speed_, int maxspeed_) {
		speed = speed_;
		newspeed = speed_;
		maxSpeed = maxspeed_;
	}

	public bool GetStop() {
		return stop;
	}

	public void SetStop(bool newstop) {
		newstop_ = newstop;
	}

	public int GetSpeed() {
		return speed;
	}

	public void SetNewSpeed(int newspeed_) {
		newspeed = newspeed_;
	}

	public void ApplyChanges() {
		speed = newspeed;
		stop = newstop_;
	}

	public Cell GetCell() {
		return cell_;
	}

	public void SetCell(Cell cell) {
		cell_ = cell;
	}

}

public class ElementOfChance{
	
	public ElementOfChance(float p, float p_sts, float p_sa, float p_s, float p_c) {
			P = p;
			P_s = p_s;
			P_sts = p_sts;
			P_sa = p_sa;
			P_c = p_c;
		}
	public float P  {
		get;
		set;
	}
	public float P_sts {
		get;
		set;
	}
	public float P_sa {
		get;
		set;
	}
	public float P_s {
		get;
		set;
	}
	public float P_c {
		get;
		set;
	}

}

//имена параметров взяты из мам.модели

	public class Parameters{

	private int maxSpeedInLaneRoad;
	private float p_slowdown;
	private float p_sts_slowstart;
	private float p_s_overspeed;
	private float p_sa_warn;
	private float p_c;
	private float p_t;

	public int MaxSpeedInLaneRoad{
		set  {
			if (value >= 0 && value < 20)
				maxSpeedInLaneRoad = value;
		}
		get { return maxSpeedInLaneRoad; }
	}

	//Вероятность правила случайного замедления
	public float P_slowdown { 
		set {
			if (value >= 0f && value < 1f)
				p_slowdown = value;
		}
		get { return p_slowdown; }
	}

	//Вероятность медленного старта 
	public float P_sts_slowstart {  
		set {
			if (value >= 0f && value < 1f) p_sts_slowstart = value;
		}
		get { return p_sts_slowstart; }
	}

	 //Вероятность превышения скорости
	public float P_s_overspeed { 
		set {
			if (value >= 0f && value < 1f)
				p_s_overspeed = value;
		}
		get { return p_s_overspeed; }
	}

	// Вероятность правилa упреждающего изменения скорости
	public float P_sa_warn { 
		set
		{
			if (value >= 0f && value <1f)
				p_sa_warn = value;
		}
		get { return p_sa_warn; }
	}

	// дистанциякоторую нужно набрать до впередиидущей машины при медленном старте
	public int D_sts { 
		get;
		set;
	}

	 // дистанциякоторую нужно набрать до впередиидущей машины при правеле упреждающего изменения скорости
	public int D_sa {
		get;
		set;
	}

	 // возможность полной остановки
	public bool SSA {
		get;
		set;
	}

	//верхний предел количества тактов без конфликта, свыше которого торможение не целесообразно
	public int Z_u {
		get;
		set;
	}   

	// коэффициент агрессивности торможения 
	public decimal K_a {
		get;
		set;
	} 
}

public class Road {

	private List<Cell> Cells;
	private List<Vehicle> Vehicles;

	private Parameters parameters_;

	public Road(Parameters parameters) {
		parameters_ = parameters;
	}

	private Vehicle GetNextVehicle(Vehicle vehicle) {
		var machineIndex = Vehicles.IndexOf(vehicle);
		if (machineIndex == Vehicles.Count - 1) return null; 
		return Vehicles[machineIndex + 1];
	}

	private int GetDistance(int machineIndex) {
		if (Vehicles[machineIndex] != null) {
			var NextvehicleCellIndex = 0;
			Vehicle Nextvehicle;
			Cell NextvehicleCell;
			Cell vehicleCell = Vehicles[machineIndex].GetCell();
			var indexCell = Cells.IndexOf(vehicleCell);
			bool BarrExist = false;
			int distansBarr = 0;
			int DestansoffNull = 0;
			for (int i = indexCell; i < Cells.Count; i++) {
				if (Cells[i].Barrier == 1) {
					BarrExist = true;
					distansBarr = i - indexCell;
					DestansoffNull = i;
					break;
				}
			}
			return distansBarr;
		}
		return 0;
	}

	public int MovevehicleInLine(ElementOfChance chance, int CellIndex) {
		if (Cells[CellIndex].GetVehicle() != null) {
			Vehicle vehicle = Cells[CellIndex].GetVehicle();
			int machineIndex = Vehicles.IndexOf(vehicle);
			int v_i_ = vehicle.GetSpeed();
			int nextvehicleDistance = GetDistance(machineIndex);
			int v_i = v_i_;
			v_i = Acceleration(v_i_, nextvehicleDistance, chance.P_sts);
			v_i = Braking(vehicle, v_i, v_i_, nextvehicleDistance, chance.P_sa);
			v_i = RandomSlow(v_i, chance.P);
			v_i = RecommSpeed(vehicle, v_i);
			v_i = SpeedOver(vehicle, v_i, v_i_, chance.P_s);
			v_i = SpeedSlowInBadLane(vehicle, v_i, v_i_);
			v_i = SlowBarrier(vehicle, v_i, v_i_);
			v_i = MaxOverSpeed(vehicle, v_i);
			v_i = NegativeStep(v_i);
			v_i = StoppingStep(v_i, vehicle);
			v_i = CollisionStep(v_i, nextvehicleDistance);
			Vehicles[machineIndex].SetNewSpeed(v_i);
			if (vehicle.GetStop() == true) Vehicles[machineIndex].SetStop(true);
			else Vehicles[machineIndex].SetStop(false);
			Vehicles[machineIndex].ApplyChanges();
			return v_i;
		} else return 0;
	}

	private int Acceleration(int v_i_, int nextvehicleDistance, float p_sts) {
		if (p_sts < parameters_.P_sts_slowstart && v_i_ == 0 &&
		nextvehicleDistance <= parameters_.D_sts) return 0;
		return v_i_ + 1;
	}

	private int Braking(Vehicle vehicle, int v_i, int v_i_, int nextvehicleDistance, float p_sa) {
		vehicle.SetStop(false);
		var nextvehicle = GetNextVehicle(vehicle);
		if (nextvehicle == null) return v_i;
		if (nextvehicle != null) {
			var v_i_d = (nextvehicle.GetSpeed()) - v_i_;
			if (p_sa < parameters_.P_sa_warn && nextvehicleDistance <= parameters_.D_sa && nextvehicle.GetStop()) {
				var z_i = Math.Floor((decimal)nextvehicleDistance / v_i_d);
				if (z_i > 0 && z_i < parameters_.Z_u)
				v_i_ = v_i_ - (int)Math.Ceiling(v_i_d * parameters_.K_a / z_i);
				if (vehicle.GetSpeed() < v_i_) vehicle.SetStop(true);
				else vehicle.SetStop(false);
				return v_i_;
			}
		}
		return v_i;
	}

	private int RandomSlow(int v_i, float p) {
		if (p < parameters_.P_slowdown) return v_i - 1;
		return v_i;
	}

	private int RecommSpeed(Vehicle vehicle, int v_i) {
		return Math.Min(v_i, Math.Min(vehicle.GetCell().MaxRulesSpeed, vehicle.GetCell().SpeedInCell));
	}

	private int SpeedOver(Vehicle vehicle, int v_i, int v_i_, float p_s) {
		var cell = vehicle.GetCell();
		if (p_s < parameters_.P_s_overspeed && v_i_ == cell.MaxRulesSpeed)
		return v_i_ + 1;
		return v_i;
	}

	private int SpeedSlowInBadLane(Vehicle vehicle, int v_i, int v_i_) {
		var cell = vehicle.GetCell();
		int indexCell = Cells.IndexOf(cell);
		int d = 0;
		if (indexCell + v_i_ > Cells.Count) d = Cells.Count - indexCell;
		else d = indexCell + v_i_;
		for (int i = indexCell; i < d; i++) {
			if (Cells[i].QualityLane < 0.9f) return v_i_ - 1;
		}
		return v_i;
	}

	private int SlowBarrier(Vehicle vehicle, int v_i, int v_i_) {
		var cell = vehicle.GetCell();
		int indexCell = Cells.IndexOf(cell);
		int d = 0;
		if (indexCell + parameters_.VisibilityDistance > Cells.Count) d = Cells.Count - indexCell;
		else d = indexCell + parameters_.VisibilityDistance;
		for (int i = indexCell; i < d; i++) {
			if (Cells[i].Barrier == 1) return v_i_ - 1;
		}
		return v_i;
	}

	private int EndLane(Vehicle vehicle, int v_i, int v_i_) {
		var cell = vehicle.GetCell();
		int indexCell = Cells.IndexOf(cell);
		if (indexCell + parameters_.VisibilityDistance > Cells.Count) return v_i_ - 1;
		return v_i;
	}

	private int MaxOverSpeed(Vehicle vehicle, int v_i) {
		return Math.Min(v_i, Math.Min(parameters_.MaxSpeedInLaneRoad, vehicle.MaxSpeedVehicle));
	}

	private int NegativeStep(int v_i) {
		if (v_i < 0) return 0;
		return v_i;
	}

	private int StoppingStep(int v_i, Vehicle vehicle) {
		if (v_i == 0 && parameters_.SSA == false) return 1;
		return v_i;
	}

	private static int CollisionStep(int v_i, int nextvehicleDistance) {
		if (v_i > nextvehicleDistance) return nextvehicleDistance;
		return v_i;
	}

}